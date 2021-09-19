SET search_path TO core;

CREATE TABLE IF NOT EXISTS core.Product(
	ProductId uuid PRIMARY KEY DEFAULT public.uuid_generate_v4(),
	ProductType INT NOT NULL,
	ProductCode VARCHAR(50) NOT NULL,
	Season INT NOT NULL,
	Price MONEY NOT NULL,
	Name VARCHAR(20) NOT NULL,
	Description VARCHAR(50) NULL,
	LaunchDate DATE NOT NULL 
);

COMMENT ON TABLE Product IS 'A clothing apparel.';
COMMENT ON COLUMN Product.ProductId IS 'The unique identifier of the product.';
COMMENT ON COLUMN Product.ProductType IS 'The type of clothing apparel. For instance: Shirt, Jeans etc.';
COMMENT ON COLUMN Product.ProductCode IS 'A unique friendly code for the product.';
COMMENT ON COLUMN Product.Season IS 'The season the product is made for. 0 = Summer, 1 = Winter, 2 = Autumn and 3 = Spring.';
COMMENT ON COLUMN Product.Price IS 'The cost of the product.';
COMMENT ON COLUMN Product.Name IS 'The name of the product.';
COMMENT ON COLUMN Product.Description IS 'A short description of the product.';
COMMENT ON COLUMN Product.LaunchDate IS 'The launch date for the product.';





