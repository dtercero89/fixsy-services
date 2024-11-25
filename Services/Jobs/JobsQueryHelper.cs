namespace FixsyWebApi.Services.Jobs
{
    public class JobsQueryHelper
    {
        public static string GetJobsPagedQuery()
        {
            return @"
            WITH PagedJobs AS (
                SELECT 
                    jb.""JobId"",
                    jb.""Title"", 
                    jb.""Description"",
                    jb.""Requirements"", 
                    jb.""Status"", 
                    jb.""PreferredSchedule"",
                    jb.""CreatedAt"",
                    u.""Name"" AS ""CustomerName"", 
                    s.""Name"" AS ""SupplierName"",
                    jba.""AssignedAt"", 
                    jba.""CompletedAt"", 
                    jba.""Notes"",
                    jba.""SupplierId"",
                    COUNT(*) OVER() AS ""TotalCount"" 
                FROM ""Jobs"" jb
                LEFT JOIN ""JobAssignments"" jba ON jba.""JobId"" = jb.""JobId""
                LEFT JOIN ""Customers"" ct ON ct.""CustomerId"" = jb.""CustomerId""
                LEFT JOIN ""Users"" u ON u.""UserId"" = ct.""UserId""
                LEFT JOIN ""Suppliers"" s ON s.""SupplierId"" = jba.""SupplierId""
                WHERE (
                    @SearchValue IS NULL OR @SearchValue = '' OR
                    jb.""Title"" ILIKE '%' || @SearchValue || '%' OR
                    jb.""Description"" ILIKE '%' || @SearchValue || '%' OR
                    jb.""Requirements"" ILIKE '%' || @SearchValue || '%' OR
                    u.""Name"" ILIKE '%' || @SearchValue || '%' OR
                    s.""Name"" ILIKE '%' || @SearchValue || '%'
                )
                AND (@Status IS NULL OR @Status = '' OR jb.""Status"" = @Status)
                AND (@CustomerId = 0 OR jb.""CustomerId"" = @CustomerId)
            )
            SELECT *
            FROM PagedJobs
            ORDER BY ""CreatedAt"" DESC -- Cambia si necesitas otro criterio de orden
            LIMIT @PageSize OFFSET (@PageNumber - 1) * @PageSize;

                ";
        }

        public static string GetJobByIdQuery()
        {
            return @"
                SELECT 
                    jb.""JobId"",
                    jb.""Title"", 
                    jb.""Description"",
                    jb.""Requirements"", 
                    jb.""Status"", 
                    jb.""PreferredSchedule"",
                    jb.""CreatedAt"",
                    u.""Name"" AS ""CustomerName"", 
                    s.""Name"" AS ""SupplierName"",
                    jba.""AssignedAt"", 
                    jba.""CompletedAt"", 
                    jba.""Notes"",
                    jba.""SupplierId"",
                    ss.""ServiceId"",
                    ss.""Name"" ""ServiceName"",
                    s.""Email"" as ""SupplierEmail"",
                    s.""PhoneNumber"" as ""SupplierPhoneNumber""
                FROM ""Jobs"" jb
                LEFT JOIN ""Services"" ss ON ss.""ServiceId"" = jb.""ServiceId""
                LEFT JOIN ""JobAssignments"" jba ON jba.""JobId"" = jb.""JobId""
                LEFT JOIN ""Customers"" ct ON ct.""CustomerId"" = jb.""CustomerId""
                LEFT JOIN ""Users"" u ON u.""UserId"" = ct.""UserId""
                LEFT JOIN ""Suppliers"" s ON s.""SupplierId"" = jba.""SupplierId""
                WHERE  jb.""JobId"" = @JobId
                AND (@CustomerId = 0 OR jb.""CustomerId"" = @CustomerId)
                ";
        }

        public static string GetCustomerJobsPagedQuery()
        {
            return @"
            WITH PagedJobs AS (
                SELECT 
                    jb.""JobId"",
                    jb.""Title"", 
                    jb.""Description"",
                    jb.""Requirements"", 
                    jb.""Status"", 
                    jb.""PreferredSchedule"",
                    jb.""CreatedAt"",
                    jb.""CustomerId"",
                    u.""Name"" AS ""CustomerName"", 
                    s.""Name"" AS ""SupplierName"",
                    jba.""AssignedAt"", 
                    jba.""CompletedAt"", 
                    jba.""Notes"",
                    COUNT(*) OVER() AS ""TotalCount"" -- Contador total de resultados
                FROM ""Jobs"" jb
                LEFT JOIN ""JobAssignments"" jba ON jba.""JobId"" = jb.""JobId""
                LEFT JOIN ""Customers"" ct ON ct.""CustomerId"" = jb.""CustomerId""
                LEFT JOIN ""Users"" u ON u.""UserId"" = ct.""UserId""
                LEFT JOIN ""Suppliers"" s ON s.""SupplierId"" = jba.""SupplierId""
                WHERE (
                    @SearchValue IS NULL OR @SearchValue = '' OR
                    jb.""Title"" ILIKE '%' || @SearchValue || '%' OR
                    jb.""Description"" ILIKE '%' || @SearchValue || '%' OR
                    jb.""Requirements"" ILIKE '%' || @SearchValue || '%' OR
                    u.""Name"" ILIKE '%' || @SearchValue || '%' OR
                    s.""Name"" ILIKE '%' || @SearchValue || '%'
                )
                AND (@Status IS NULL OR @Status = '' OR jb.""Status"" = @Status)
                AND jb.""CustomerId"" = @CustomerId
            )
            SELECT *
            FROM PagedJobs
            ORDER BY ""CreatedAt"" DESC
            LIMIT @PageSize OFFSET (@PageNumber - 1) * @PageSize;

                ";
        }

        public static string GetSummaryJobs()
        {
            return @"SELECT
                    COUNT(CASE WHEN ""Status"" = 'IN PROCESS' THEN 1 ELSE NULL END) AS ""InProcess"",
                    COUNT(CASE WHEN ""Status"" = 'PENDING' THEN 1 ELSE NULL END) AS ""Pending""
                FROM ""Jobs""
                WHERE @CustomerId =0 OR ""CustomerId"" = @CustomerId;
                ";
        }
    }
}
