// Fix for CS0246, IDE0060: Remove 'subImg' and 'color' parameters from Create action as they are unused and 'IForfile' is undefined.
[HttpPost]
public IActionResult Create(Product Product, IFormFile img)
{
    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imeges", fileName);
    // save Img in wwwroot

    using(var stream = System.IO.File.Create(filePath))
    {
        img.CopyTo(stream);
    }

    Product.MainImg = fileName;

    //save Product in db
    _db.Products.Add(Product);
    _db.SaveChanges();
    return RedirectToAction(nameof(Index));
}