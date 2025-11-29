// Fix for CS0815: Cannot assign void to an implicitly-typed variable
// The problematic line:
// var ProductinDB = await _productRepository.GetAsync(Product.Id, cancellationToken);

// According to the signature, ProductRepository.GetAsync(int id, CancellationToken cancellationToken) returns void.
// You should use the correct method to get a Product by id. Based on the CategoryRepository<T> signature, use GetOneAsync:

var ProductinDB = await _productRepository.GetOneAsync(
    e => e.Id == Product.Id,
    includes: [(e => e.Category), (e => e.Brand)],
    tracked: true,
    cancellationToken: cancellationToken
);