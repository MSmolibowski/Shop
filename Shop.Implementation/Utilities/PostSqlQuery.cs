using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Implementation.Utilities;
public class PostSqlQuery
{
    // Product
    public const string GET_ALL_PRODUCTS = @"Select id, name, description 
                                                From products;";
    
    public const string INSERT_PRODUCT = @"Insert Into products(name, description)
                                            Values(@Name, @Description)
                                            Returning id;";
    
    public const string GET_PRODUCTS_BY_CATEGORY = @"Select *
                                                    From products As p
                                                    Join products_categories As pc
                                                    On pc.product_id = p.id
                                                    Where pc.category_id = Any(@CategoryIds::int[]);";

    // Category
    public const string GET_CATEGORY_ID = @"Select id
                                            From category
                                            Where name = Any(@CategoryNames::text[]);";

    public const string INSERT_CATEGORY = @"Insert Into category(name, description)
                                            Values(@Name, @Description)
                                            Returning id";

    // Mapping
    public const string INSERT_MAPPING = @"Insert Into products_categories(product_id, category_id)
                                            Values (@ProductId, @CategoryId);";
}
