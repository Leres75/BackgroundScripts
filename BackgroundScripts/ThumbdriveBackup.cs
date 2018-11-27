using System;
using System.IO;
using System.Linq;

namespace BackgroundScripts
{
    class ThumbdriveBackup
    {
        public static void Backup()
        {
            var drives = DriveInfo.GetDrives().Where(drive => drive.DriveType.Equals(DriveType.Removable));
            DirectoryInfo destDirectory;
            foreach (DriveInfo d in drives)
            {
                destDirectory = new DirectoryInfo(@"D:\USBBackups\" + d.VolumeLabel + @"\");
                DirectoryInfo sourceDirectory = d.RootDirectory;
                CopyFolder(sourceDirectory, destDirectory);
            }
        }

        public static void CopyFolder(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                CopyFolder(dir, target.CreateSubdirectory(dir.Name));
            }

            foreach (FileInfo file in source.GetFiles())
            {
                if(File.Exists(Path.Combine(target.FullName, file.Name) )){
                    FileInfo targetFile = new FileInfo(Path.Combine(target.FullName, file.Name));
                    if(file.LastWriteTime != targetFile.LastWriteTime)
                    {
                        file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                        targetFile.LastWriteTime = file.LastWriteTime;
                    }
                }
                else
                {
                    file.CopyTo(Path.Combine(target.FullName, file.Name));
                    FileInfo targetFile = new FileInfo(Path.Combine(target.FullName, file.Name));
                    try{
                        targetFile.LastWriteTime = file.LastWriteTime;
                    }catch(UnauthorizedAccessException e)
                    {
                        continue;
                    }
                }
            }
        }
    }
}
