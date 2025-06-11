namespace Shop.Implementation.Utilities;
public class PostSqlQuery
{
    // Product
    public const string GET_ALL_PRODUCTS = @"Select id, name, description 
                                                From products;";

    public const string GET_PRODUCTS_BY_CATEGORY_NAME = @"Select p.*
                                                    From products As p
                                                    Join products_categories As pc
                                                        On pc.product_id = p.id
                                                    Join category As c
                                                        On c.id = pc.category_id
                                                    Where c.name = @CategoryName;";

    public const string GET_PRODUCT_BY_NAME = @"Select id, name, description
                                                From products
                                                Where name = @Name;";

    public const string INSERT_PRODUCT = @"Insert Into products(name, description)
                                            Values(@Name, @Description)
                                            Returning id;";

    public const string DELETE_PRODUCT_BY_NAME = @"Delete From products                
                                                    Where name = @Name
                                                    Returning id;"; // ON DELETE CASCADE (USUWANIE Z PRODUCTS I PRODUCTS_CATEGORIES AUTOMATYCZNE ( w migrations onDelete: ReferentialAction.Cascade));


    //public const string DELETE_PRODUCT2 = @"With deleted AS (         # Use when onDelete: ReferentialAction.Cascade is not set for products_categories
    //                                        Delete From products
    //                                        Where name = @Name
    //                                        Returning id)   
    //                                        Delete From products_categories
    //                                        Where product_id In (Select id From deleted);";

    //public const string DELETE_PRODUCT_BY_NAME

    // Category
    public const string GET_ALL_CATEGORIES = @"Select id, name, description
                                                    From category;";

    public const string GET_ALL_CATEGORY_NAMES = @"Select name
                                                    From category;";
    
    public const string GET_CATEGORY_ID = @"Select id
                                            From category
                                            Where name = Any(@CategoryNames::text[]);";

    public const string GET_CATEGORY_BY_NAME = @"Select *
                                                 From category
                                                 Where name = @Name;";

    public const string INSERT_CATEGORY = @"Insert Into category (name, description)
                                            Values(@Name, @Description)
                                            Returning id, name, description;";

    public const string DELETE_CATEGORY_BY_NAME = @"Delete From category
                                            Where name = @Name
                                            Returning id;";

    // Mapping
    public const string INSERT_MAPPING = @"Insert Into products_categories(product_id, category_id)
                                            Values (@ProductId, @CategoryId);";
}
