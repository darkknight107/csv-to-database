SET search_path TO core;

CREATE TABLE IF NOT EXISTS core.ProductItem(
	ProductItemId uuid PRIMARY KEY DEFAULT public.uuid_generate_v4(),
	ProductItemCode VARCHAR(50) UNIQUE NOT NULL,
	Size int NOT NULL,
	Barcode VARCHAR(13) NOT NULL,
	ProductId uuid NOT NULL REFERENCES core.Product (productId)
);

COMMENT ON TABLE ProductItem IS 'The actual product item.';
COMMENT ON COLUMN ProductItem.ProductItemId IS 'The unique identifier of the product item.';
COMMENT ON COLUMN ProductItem.ProductItemCode IS 'The unique friendly code for the product item.';
COMMENT ON COLUMN ProductItem.Size IS 'The size of the product item. 0 = XS, 1 = S, 2 = M, 3 = L, 4 = XL and 5 = XXL';
COMMENT ON COLUMN ProductItem.Barcode IS 'The barcode of the product item.';


