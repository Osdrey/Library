namespace Library.Infraestructure.Queries
{
    public static class MaterialQueries
    {
        public const string GetAvailableMaterials =
            "SELECT * FROM Materials WHERE MaterialStatus = 0";

        public const string GetAllMaterials = @"
            SELECT * FROM Materials
            WHERE 
                Title LIKE '%' + @input + '%' OR
                Author LIKE '%' + @input + '%' OR
                CAST(PublicationYear AS NVARCHAR) LIKE '%' + @input + '%'";

        public const string GetMaterial =
            "SELECT * FROM Materials WHERE MaterialId = @materialId";

        public const string IsMaterialAvailable =
            "SELECT MaterialStatus FROM Materials WHERE MaterialId = @materialId";

        public const string InsertMaterial = @"
            INSERT INTO Materials
                (Title, Author, PublicationYear, MaterialStatus, MaterialCondition, MaterialTopic, Pages, Format, Duration, MaterialType)
            VALUES
                (@Title, @Author, @PublicationYear, @MaterialStatus, @MaterialCondition, @MaterialTopic, @Pages, @Format, @Duration, @MaterialType)";

        public const string UpdateMaterial = @"
            UPDATE Materials SET
                Title = @Title,
                Author = @Author,
                PublicationYear = @PublicationYear,
                MaterialStatus = @MaterialStatus,
                MaterialCondition = @MaterialCondition,
                MaterialTopic = @MaterialTopic,
                Pages = @Pages,
                Format = @Format,
                Duration = @Duration
            WHERE MaterialId = @MaterialId";

        public const string UpdateMaterialStatus =
            "UPDATE Materials SET MaterialStatus = @status WHERE MaterialId = @materialId";

        public const string DeleteMaterial =
            "DELETE FROM Materials WHERE MaterialId = @materialId";
    }

}
