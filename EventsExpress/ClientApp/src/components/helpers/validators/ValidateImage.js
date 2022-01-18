export default function ValidateImage(file) {
  const tenMegaBytesInBytes = 1024 * 1024 * 10;
  const allowedExtensions = ["jpeg", "jpg", "png", "bmp"];

  if (file.size > tenMegaBytesInBytes) 
    return "File size can not exceed 10 MB";
  if (!allowedExtensions.some((el) => file.path.endsWith(el)))
    return "Accepted file formats are .jpeg, .jpg, .png, or .bmp";
}
