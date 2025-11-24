public IActionResult Index(FilterProductVM filter)
{
    var Products = _db.Products.AsNoTracking().AsQueryable();
    Products = Products.Include(e => e.Category).Include(b => b.Brand);

    var Product = _db.Products.Include(e => e.Category).AsQueryable();

    if (filter.name is not null)
        Product = Product.Where(e => e.Name.Contains(filter.name));

    if (filter.minprice is not null)
        Product = Product.Where(e => e.Price - e.Price * e.Discount / 100 > filter.minprice);

    if (filter.maxprice is not null)
        Product = Product.Where(e => e.Price - e.Price * e.Discount / 100 < filter.maxprice);

    if (filter.categotyId is not null)
        Product = Product.Where(e => e.CategoryID == filter.categotyId);

    if (filter.brandId is not null)
        Product = Product.Where(e => e.BrandID == filter.brandId);

    var categories = _db.categories.AsEnumerable();
    ViewBag.categories = _db.categories;

    var Brands = _db.Brands.AsEnumerable();
    ViewBag.Brands = _db.Brands;

    return View(Product.AsEnumerable());
}