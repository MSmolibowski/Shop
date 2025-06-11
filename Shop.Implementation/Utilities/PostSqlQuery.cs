namespace Shop.Implementation.Utilities;
public class PostSqlQuery
{
    // Product
    public const string GET_ALL_PRODUCTS = @"Select id, name, description // Czyzawsze chcemy pobierać rekordy z wartościami powiązanymi, czy solo też?
                                                From products;";

    public const string GET_ALL_PRODUCTS_WITH_CATEGORIES = @"
                                                            SELECT 
                                                                p.id, 
                                                                p.name, 
                                                                p.description, 
                                                                array_remove(array_agg(c.name), NULL) AS ""Categories""
                                                            FROM products p
                                                            LEFT JOIN 
                                                                products_categories pc ON p.id = pc.product_id
                                                            LEFT JOIN 
                                                                category c ON pc.category_id = c.id
                                                            GROUP BY 
                                                                p.id, p.name, p.description;";


    public const string GET_PRODUCTS_BY_CATEGORY_NAME = @"Select p.*
                                                    From products As p
                                                    Join products_categories As pc
                                                        On pc.product_id = p.id
                                                    Join category As c
                                                        On c.id = pc.category_id
                                                    Where c.name = @CategoryName;";

    public const string GET_PRODUCTS_BY_CATEGORY_NAME_WITH_CATEGORIES = @"
                                                                        SELECT 
                                                                            p.id, 
                                                                            p.name, 
                                                                            p.description, 
                                                                            array_remove(array_agg(c.name), NULL) AS ""Categories""
                                                                        FROM 
                                                                            products p
                                                                        INNER JOIN 
                                                                            products_categories pc ON p.id = pc.product_id
                                                                        INNER JOIN 
                                                                            category c ON pc.category_id = c.id
                                                                        WHERE 
                                                                            p.id IN (
                                                                                SELECT p2.id
                                                                                FROM products p2
                                                                                INNER JOIN products_categories pc2 ON p2.id = pc2.product_id
                                                                                INNER JOIN category c2 ON c2.id = pc2.category_id
                                                                                WHERE c2.name = @CategoryName
                                                                            )
                                                                        GROUP BY 
                                                                            p.id, p.name, p.description;";

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
