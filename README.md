--user table
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(100) NOT NULL
);

select * from Users

--sp_registration procedure
CREATE PROCEDURE sp_RegisterUser113
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Password NVARCHAR(100),
    @ReturnVal INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
    BEGIN
        SET @ReturnVal = -1;  
        RETURN;
    END

    INSERT INTO Users (Name, Email, Password)
    VALUES (@Name, @Email, @Password);

    SET @ReturnVal = SCOPE_IDENTITY();
END


--sp_login procedure
CREATE PROCEDURE sp_LoginUser1
    @Email VARCHAR(100),
    @Password VARCHAR(100)
AS
BEGIN
    SELECT Id, Email
    FROM Users
    WHERE Email = @Email AND Password = @Password
END


--product table
CREATE TABLE Product (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL, 
    Price DECIMAL(10,2) NOT NULL,
    Photo NVARCHAR(255) NOT NULL         
);
select * from product
 INSERT INTO Product (Name, Description, Price, Photo) 
VALUES 
('Laptop', 'A laptop is a portable computer that can be easily carried around.', 1500.99, 'img1.jpg'),
('OnePlus 2T', 'OnePlus Nord 2 5G Android smartphone.', 150.99, 'img2.jpg'),
('SmartWatch', 'A smartwatch is a portable device worn on the wrist.', 15.99, 'img3.jpg'),
('Sunglass', 'They are usually made of a plastic or metal frame and two lenses that are darkened to filter out light.', 9.99, 'img4.jpg'),
('Winter Jacket', 'A good winter jacket is defined by its warmth and lightweight design.', 10.99, 'img5.jpg'),
('Soft Toys', 'A toy or plaything is an object that is used primarily to provide entertainment.', 5.99, 'img6.jpg'),
('Water Bottle', 'Water bottle is a container which is used to carry and keep wa ter or any liquid in stable temperature for consumption.', 1.99, 'img7.jpg'),
('Trigger EarBuds', 'The Triggr Ear Buds are not only aesthetically pleasing but also incredibly convenient, featuring a compact and lightweight.', 4.99, 'img8.jpg');


--cart table
   CREATE TABLE Cart (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT FOREIGN KEY REFERENCES Users(Id) ON DELETE CASCADE,
    ProductId INT FOREIGN KEY REFERENCES Product(Id) ON DELETE CASCADE
);



CREATE PROCEDURE sp_GetCartItemsByUserId
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.Id AS CartId,
        c.UserId,
        c.ProductId,
        p.Name AS ProductName,
        p.Price,
        p.Photo
    FROM 
        Cart c
    JOIN 
        Product p ON c.ProductId = p.Id
    WHERE 
        c.UserId = @UserId;
END;

select * from Cart
 




alter PROCEDURE AddToCart
    @ItemId INT
	@ItemQuantity INT
AS
BEGIN
   
    -- Update the CartStatus of the item with the provided ItemId to 1 (indicating it's added to cart)
    UPDATE Items
    SET IsItemCarted = 1 
    WHERE ItemId = @ItemId;

	UPDATE Items
    SET ItemQuantity = ItemQuantity - 1
    WHERE ItemId = @ItemId AND ItemQuantity > 0; 

	UPDATE Items
    SET CartItemQuantity = CartItemQuantity + 1
    WHERE ItemId = @ItemId;
END;
 















  























