-- Criação de Tabelas

create table if not exists Products (
    Id int primary key auto_increment,
    Name varchar(100) not null,
    Description varchar(200),
    Price decimal(15, 2) not null,
    StockQuantity int not null
);

create table if not exists Orders (
    Id int primary key auto_increment,
    CustomerName varchar(100) not null,
    CreatedAt datetime not null,
    TotalAmount decimal(15, 2) not null
);

create table if not exists OrderItems (
    Id int primary key auto_increment,
    OrderId int not null,
    ProductId int not null,
    Quantity int not null,
    UnitPrice decimal(15, 2) not null,
    foreign key (OrderId) references Orders(Id) on delete cascade,
    foreign key (ProductId) references Products(Id) on delete restrict
);