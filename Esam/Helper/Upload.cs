namespace Esam.Helper;
public static class Upload // جعل الكلاس static أفضل بما أن الميثودز static
{
    public static string UploadFile
    (
        string folderName,
        IFormFile file
    ){
        try{
            // 1. تحديد مسار المجلد والتأكد من وجوده
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            // 2. إنشاء اسم فريد للملف
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            // 3. حفظ الملف (استخدام using لضمان غلق الملف بعد الرفع)
            using (var stream = new FileStream(filePath, FileMode.Create)){ file.CopyTo(stream); }

            return fileName;
        }
        catch (Exception e){
            // يفضل استخدام Logger بدلاً من Console في تطبيقات الويب
            Console.WriteLine($"Error uploading file: {e.Message}");
            return string.Empty;
        }
    }

    public static bool RemoveFile
    (
        string folderName,
        string fileName
    ){
        try{
            if (string.IsNullOrEmpty(fileName)) return false;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);

            if (File.Exists(filePath)){
                File.Delete(filePath);
                return true;
            }
            return false;
        }
        catch (Exception e){
            Console.WriteLine($"Error deleting file: {e.Message}");
            return false;
        }
    }
}
