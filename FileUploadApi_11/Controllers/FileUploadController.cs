using AutoMapper;
using ExcelDataReader;
using FileUploadApi_11.Data;
using FileUploadApi_11.Dto;
using FileUploadApi_11.Dto.Repository;
using FileUploadApi_11.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Net.Http.Headers;
using static FileUploadApi_11.Models.BatchUploadDetail;
using static System.Net.WebRequestMethods;

namespace FileUploadApi_11.Controllers
{
    [Route("api")]
    [ApiController]
    public class FileUploadController : Controller
    {

        public string ErrorMessage { get; set; }


        private readonly ApplicationDbContext _context;
        private readonly IBatchUploadRepository _batchUploadRepository;
        private readonly IMapper _mapper;
        public IConfiguration _configuration;
        public FileUploadController(ApplicationDbContext context, IBatchUploadRepository batchUploadRepository, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _batchUploadRepository = batchUploadRepository;
            _mapper = mapper;
            this._configuration = configuration;
        }

        [HttpGet]
        [Route("UserDetails")]
        public IActionResult GetUserDetails()
        {

            var userDetails = _context.UserDetails.ToList();
            return Ok(userDetails);

        }
        [HttpGet]
        [Route("BatchUploadDetails")]
        public IActionResult BatchUploadDetails()
        {

            //var userDetails = _context.BatchUploadDetails.ToList();

            var userDetails = _batchUploadRepository.GetBatchUploadsDetails().ToList().Select(_mapper.Map<BatchUploadDetail, BatchUploadDto>);

            return Ok(userDetails);

        }

        [HttpPost]
        [Route("FileUpload")]
        public async Task<IActionResult> FileUpload(IFormFile File, [FromForm] BatchUploadDto batchUploadDto)
        {


            var fileuploadId = await _context.BatchUploadDetails.Where(x => x.FileUploadId == batchUploadDto.FileUploadId).AnyAsync();
            var FileName = File.FileName;

            if (!fileuploadId)
            {
                if (File.Length > 0)
                {
                    //var supportedTypes = new[] { "txt", "doc", "docx", "pdf", "xls", "xlsx" };

                    var supportedTypes = new[] { "xls" };
                    var fileExt = System.IO.Path.GetExtension(File.FileName).Substring(1);

                    if (!supportedTypes.Contains(fileExt))
                    {
                        ErrorMessage = "File Extension Is InValid - Only Upload xls File";
                        return Ok(ErrorMessage);
                    }

                    else
                    {

                        var localPath = Path.Combine("TemporaryFile/");
                        var fileNamee = File.FileName;
                        var setDrivePath = Path.Combine(localPath + fileNamee);

                        //string[] getfile    = File.Length.ToString().Split('\u002C');
                        string[] getfile = File.Length.ToString().Split('\u002c');


                        using (var stream = new FileStream(setDrivePath, FileMode.Create))
                        {
                            File.CopyTo(stream);
                        }


                        string conString = _configuration.GetConnectionString("ExcelConString");


                        conString = string.Format(conString, setDrivePath);
                        DataTable dt = new DataTable();

                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.
                                    connExcel.Open();
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    connExcel.Close();

                                    //Read Data from First Sheet.
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();
                                }
                            }
                        }




                        //Insert the Data read from the Excel file to Database Table.
                        var ConnectionString = _configuration.GetConnectionString("defaultConnection");
                        using (SqlConnection con = new SqlConnection(ConnectionString))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                sqlBulkCopy.DestinationTableName = "dbo.employees";
                                //[OPTIONAL]: Map the Excel columns with that of the database table
                                sqlBulkCopy.ColumnMappings.Add("First Name", "FirstName");
                                sqlBulkCopy.ColumnMappings.Add("Last Name", "LastName");
                                sqlBulkCopy.ColumnMappings.Add("Gender", "Gender");
                                sqlBulkCopy.ColumnMappings.Add("Country", "Country");
                                sqlBulkCopy.ColumnMappings.Add("Age", "Age");
                                sqlBulkCopy.ColumnMappings.Add("Date", "Date");

                                con.Open();
                                sqlBulkCopy.WriteToServer(dt);
                                con.Close();
                            }
                        }


                        string getFile = Path.Combine(Directory.GetCurrentDirectory() + "/TemporaryFile/");
                        string[] Files = Directory.GetFiles(getFile).ToArray();

                        foreach (string file in Files)
                        {

                            if ((System.IO.File.Exists(file)))
                            {
                                System.IO.File.Delete(file);
                            }

                        }

                        //


                        if (batchUploadDto.TempleteTypes == "e")
                        {
                            //var enrollmentPath  = _configuration.GetSection("Path").GetSection("Enrollment").Value;
                            //var folderName = Path.Combine(enrollmentPath);
                            var path = _configuration.GetSection("DrivePath").Value;
                            var folderName = Path.Combine(path + "Enrollment");
                            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                            //var fileName = ContentDispositionHeaderValue.Parse(File.ContentDisposition).FileName.Trim('"');
                            var fileName = File.FileName;
                            FileSave(File, folderName, pathToSave);
                        }


                        else if (batchUploadDto.TempleteTypes == "c")
                        {

                            //var cancelllationPath  = _configuration.GetSection("Path").GetSection("Cancellation").Value;
                            //var folderName = Path.Combine(cancelllationPath);
                            var path = _configuration.GetSection("DrivePath").Value;
                            var folderName = Path.Combine(path + "Cancellation");
                            //var folderName = Path.Combine(@"E:/BatchUpload/Cancellation");
                            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                            FileSave(File, folderName, pathToSave);


                        }

                        else if (batchUploadDto.TempleteTypes == "s")
                        {
                            //var suspensionPath  = _configuration.GetSection("Path").GetSection("Suspension").Value;
                            //var folderName = Path.Combine(suspensionPath);
                            var path = _configuration.GetSection("DrivePath").Value;
                            var folderName = Path.Combine(path + "Suspension");
                            //var folderName = Path.Combine(@"E:/BatchUpload/Suspension");
                            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                            FileSave(File, folderName, pathToSave);

                        }

                        else
                        {
                            return Ok(ErrorMessage = "Templete Type not Matched");
                        }

                    }

                }
                else
                {
                    return Ok(ErrorMessage = "File not selected ");
                }



            }
            else
                return BadRequest("File Upload Id Is already exist");


            var accountCode = await _context.UserDetails.Where(r => r.Id == batchUploadDto.UserId).Select(r => r.AccountCode).FirstOrDefaultAsync();
            if (accountCode == null)
                return Ok(ErrorMessage = "Account code doesn't exist for user");


            BatchUploadDetail uploadDetail = new BatchUploadDetail();

            uploadDetail.AccountCode = accountCode;
            uploadDetail.FileUploadId = batchUploadDto.FileUploadId;
            uploadDetail.UserId = batchUploadDto.UserId;
            uploadDetail.FileName = FileName;
            uploadDetail.TempleteTypes = batchUploadDto.TempleteTypes;
            _context.BatchUploadDetails.Add(uploadDetail);

            _context.SaveChanges();
            return Ok(uploadDetail);

        }

        private static void FileSave(IFormFile File, string folderName, string pathToSave)
        {
            var fileName = File.FileName;
            var setDrivePath = Path.Combine(pathToSave, fileName);
            using (var stream = new FileStream(setDrivePath, FileMode.Create))
            {
                File.CopyTo(stream);
            }
            
            
            // Multiple File upload
            
            
                var path = _configuration.GetSection("DrivePath").Value;
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), path);


            foreach (var f in fileData.Files)
            {

                var setDrivePath = Path.Combine(pathToSave, f.FileName);

                using (var stream = new FileStream(setDrivePath, FileMode.Create))
                {
                    f.CopyTo(stream);
                    
                }

            }
            // close
            
            
            
        }

    }


}


