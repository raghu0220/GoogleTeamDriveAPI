
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Requests;

namespace DriveQuickstart
{
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json

        // test commit
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "Drive API .NET Quickstart";

        static void Main(string[] args)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = @"C:\Users\raghavendra\source\repos\UploadToTeamDrive\UploadToTeamDrive\text.json";

               // HttpPostedFileBase obj = new 
                //Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });








            /*








            string path = @"C:\Users\raghavendra\source\repos\UploadToTeamDrive\UploadToTeamDrive\bin\Debug\files\my.pdf";
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"C:\Users\raghavendra\source\repos\UploadToTeamDrive\UploadToTeamDrive\bin\Debug\files\my.pdf");
            // byte[] fileBytes = new byte[stream.Length];
            // HttpPostedFileBase file;
            // string path = @"C:\Users\raghavendra\source\repos\UploadToTeamDrive\UploadToTeamDrive\bin\Debug\files\my.pdf";


            /*
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
            Path.GetFileName(file.FileName));
            file.SaveAs(path); */


            var FileMetaData = new Google.Apis.Drive.v3.Data.File();
            FileMetaData.Name = Path.GetFileName(path);
            FileMetaData.MimeType = MimeMapping.GetMimeMapping(path);

            FilesResource.CreateMediaUpload request;

          /*  using (FileStream fs = System.IO.File.Open(path, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(path);
                // Add some information to the file.

                request = service.Files.Create(FileMetaData, info, FileMetaData.MimeType);
                request.Fields = "id";
                request.Upload();
                fs.Write(info, 0, info.Length);
            }


            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                request = service.Files.Create(FileMetaData, fileStream, FileMetaData.MimeType);
                request.Fields = "id";
                request.Upload();
            }


            */


            using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                {
                    request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }
                
            var file = request.ResponseBody;
            Console.WriteLine("File ID: " + file.Id);



            //HttpPostedFileBase file







            /* // Define parameters of request.
             FilesResource.ListRequest listRequest = service.Files.List();

             listRequest.PageSize = 999;
             listRequest.Fields = "nextPageToken, files(id, name)";


             // List files.
             IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                 .Files;
             Console.WriteLine("Files:");
             if (files != null && files.Count > 0)
             {
                 foreach (var file in files)
                 {
                     Console.WriteLine("{0} ({1})", file.Name, file.Id);
                 }
             }
             else
             {
                 Console.WriteLine("No files found.");
             }
             Console.Read();


             

            GoogleDriveFilesRepository.FileUpload(file);
            string path = @"C:\Users\raghavendra\source\repos\UploadToTeamDrive\UploadToTeamDrive\bin\Debug\files\my.pdf";
            var output = uploadToDrive(service, path, "root");


            var FileMetaData = new Google.Apis.Drive.v3.Data.File();
            FileMetaData.Name = "my.pdf";
                FileMetaData.MimeType = MimeMapping.GetMimeMapping(path);

                FilesResource.CreateMediaUpload request;

                using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                {
                    request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }
            var file = request.ResponseBody;
            Console.WriteLine("File ID: " + file.Id); */
        }



        //public static Google.Apis.Drive.v3.Data.File uploadToDrive(DriveService _service, string _uploadFile, string _parent = "root")
        //{

        //    if (!String.IsNullOrEmpty(_uploadFile))
        //    {

        //        Google.Apis.Drive.v3.Data.File fileMetadata = new Google.Apis.Drive.v3.Data.File();
        //        fileMetadata.Name = System.IO.Path.GetFileName(_uploadFile);
        //        fileMetadata.MimeType = MimeMapping.GetMimeMapping(_uploadFile);
        //        fileMetadata.Parents = new List<string> { "Sample" };
        //        //fileMetadata.Parents = new List<FilesResource>() { new FilesResource() {}};                    

        //        try
        //        {
        //            byte[] byteArray = System.IO.File.ReadAllBytes(_uploadFile);
        //            System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
        //            FilesResource.CreateMediaUpload request = _service.Files.Create(fileMetadata, stream, MimeMapping.GetMimeMapping(_uploadFile));
        //            request.SupportsTeamDrives = "true";
        //            request.Upload();
        //            return request.ResponseBody;

        //        }
        //        catch (System.IO.IOException iox)
        //        {
        //            // Log
        //            return null;
        //        }
        //        catch (Exception e) // any special google drive exceptions??
        //        {
        //            //Log
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        //Log file does not exist
        //        return null;
        //    }
        //}


        //public static Google.Apis.Drive.v3.Data.File createDirectory(DriveService _service, string _title, string _description, string _parent)
        //{

        //    Google.Apis.Drive.v3.Data.File NewDirectory = null;

        //    // Create metaData for a new Directory
        //    Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
        //    body.Title = _title;
        //    body.Description = _description;
        //    body.MimeType = "application/vnd.google-apps.folder";
        //    body.Parents = new List() { new ParentReference() { Id = _parent } };
        //    try
        //    {
        //        FilesResource.InsertRequest request = _service.Files.(body);
        //        NewDirectory = request.Execute();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("An error occurred: " + e.Message);
        //    }

        //    return NewDirectory;
        //}


        //private static Google.Apis.Drive.v3.Data.File insertFile(DriveService service, String title, String description, String parentId, String mimeType, String filename)
        //{
        //    // File's metadata.
        //    Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
        //    body.Name = "MyFile";
        //    body.Description = description;
        //    body.MimeType = mimeType;

        //    // Set the parent folder.
        //    if (!String.IsNullOrEmpty(parentId))
        //    {
        //        body.Parents = new List<ParentReference>()  {new ParentReference() {Id = parentId}};
        //    }

        //    // File's content.
        //    byte[] byteArray = System.IO.File.ReadAllBytes(filename);
        //    MemoryStream stream = new MemoryStream(byteArray);

        //    try
        //    {
        //        FilesResource.InsertMediaUpload request = service.Files.Insert(body, stream, mimeType);
        //        request.Upload();

        //        File file = request.ResponseBody;

        //        // Uncomment the following line to print the File ID.
        //        // Console.WriteLine("File ID: " + file.Id);

        //        return file;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("An error occurred: " + e.Message);
        //        return null;
        //    }
        //}




        /*

        var fileMetadata = new Google.Apis.Drive.v3.Data.File()
        {
            Name = "photo.jpg"
        };
        FilesResource.CreateMediaUpload request;

        using (var stream = new System.IO.FileStream(@"C:\Users\raghavendra\source\repos\UploadToTeamDrive\UploadToTeamDrive\files\photo.jpeg",
                                System.IO.FileMode.Open))
        {
            request = service.Files.Create(
                fileMetadata, stream, "image/jpeg");
            request.uploadType = resumable;
            request.Fields = "id";
            request.Upload();
        }
        var file = request.ResponseBody;
        Console.WriteLine("File ID: " + file.Id);


        */



        /* // List files.
         IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
             .Files;
         Console.WriteLine("Files:");
         if (files != null && files.Count > 0)
         {
             foreach (var file in files)
             {
                 Console.WriteLine("{0} ({1})", file.Name, file.Id);
             }
         }
         else
         {
             Console.WriteLine("No files found.");
         }
         Console.Read(); */

    }
}

