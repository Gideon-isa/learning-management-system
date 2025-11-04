using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Features.Lesson.Command;
using Lms.CourseManagement.Domain.ValueObjects;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Lms.CourseManagement.Application.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        public LocalFileStorageService()
        {
            
        }

        public async Task<string> GetFilePath(string ContentFileName, string folderPath, CancellationToken cancellationToken)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ContentFileName);
            var filePath = Path.Combine(folderPath, fileName);

            return await Task.FromResult(filePath);
        }

        public async Task<string> GetFolderPath(CreateLessonCommand command, CancellationToken cancellationToken)
        {
            var baseDirectory = "C:\\LMS_Strorage";
            Directory.CreateDirectory(baseDirectory); // Ensure base directory exists
            var folderPath = Path.Combine(baseDirectory, command.Title);
            Directory.CreateDirectory(folderPath); // Ensure folder exists

             return await Task.FromResult(folderPath);

        }

        public async Task<IEnumerable<LessonImage>> UploadImageFileAsync(CreateLessonCommand command, CancellationToken cancellationToken)
        {
            var folderPath = await GetFolderPath(command, cancellationToken);
            
            List<LessonImage> ImageFiles = [];

            for (int i = 0; i < command.Images.Count; i++)
            {
                var filePath = await GetFilePath(command.Images[i].FileName, folderPath, cancellationToken);
                using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

                command.Images[i].Content.CopyTo(fileStream); // saved the image stream to the file

                ImageFiles.Add( new LessonImage(command.Images[i].FileName, filePath, command.Images[i].Caption));
            }
            return ImageFiles;
        }


        public async Task<string> UploadVideoFileAsync(CreateLessonCommand command, CancellationToken cancellationToken)
        {
            var folder = await GetFolderPath(command, cancellationToken);
            var filePath = await GetFilePath(command.Video.FileName, folder, cancellationToken);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                command.Video.Content.CopyTo(fileStream);
            }
            return filePath;
            // Return the file path as the URL (in a real scenario, this would be a URL)
        }


    }
}
