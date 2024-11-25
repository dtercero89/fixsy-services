namespace FixsyWebApi.Services.Suppliers
{
    public class SupplierQueryHelper
    {
        public static string GetSupplierPagedQuery()
        {
            return @"
    WITH PagedSuppliers AS (
  SELECT 
    sp.""SupplierId"", 
    sp.""Name"", 
    sp.""Email"", 
    sp.""PhoneNumber"", 
    sp.""Status"",
    STRING_AGG(ss.""Name"", ', ') AS ""Services"",
    COUNT(*) OVER() AS ""TotalCount"" -- Contador total de resultados
  FROM 
    ""Suppliers"" sp
  LEFT JOIN 
    ""SupplierServices"" sps ON sps.""SupplierId"" = sp.""SupplierId""
  LEFT JOIN 
    ""Services"" ss ON ss.""ServiceId"" = sps.""ServiceId""
  WHERE 
    (@SearchValue IS NULL OR @SearchValue = '' OR 
     sp.""Name"" ILIKE '%' || @SearchValue || '%' OR 
     sp.""Email"" ILIKE '%' || @SearchValue || '%' OR 
     sp.""Email"" ILIKE '%' || @SearchValue || '%' OR 
     ss.""Name"" ILIKE '%' || @SearchValue || '%')
  GROUP BY 
    sp.""SupplierId"", sp.""Name"", sp.""Email"", sp.""PhoneNumber"", sp.""Status""
)
SELECT *
FROM PagedSuppliers
ORDER BY ""SupplierId"" -- Cambiar si necesitas otro criterio de orden
LIMIT @PageSize OFFSET (@PageNumber - 1) * @PageSize;
            
";
        }
    }
}
