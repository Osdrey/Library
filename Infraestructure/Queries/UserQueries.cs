namespace Library.Infraestructure.Queries
{
    public static class UserQueries
    {
        public const string GetAllActiveUsers = @"
            SELECT * FROM Users
            WHERE isActive = 1";

        public const string GetUserByInput = @"
            SELECT * FROM Users
            WHERE CAST(Id AS NVARCHAR) = @input
               OR CAST(Document AS NVARCHAR) = @input
               OR Email = @input
               OR UserName = @input";

        public const string InsertUser = @"
            INSERT INTO Users
            (document, firstName, lastName, middleName, age, email, userName, password, userType, userRole, arrears, isActive)
            VALUES
            (@document, @firstName, @lastName, @middleName, @age, @email, @userName, @password, @userType, @userRole, @arrears, @isActive);";

        public const string UpdateUser = @"
            UPDATE Users SET 
                firstName = @firstName,
                lastName = @lastName,
                middleName = @middleName,
                age = @age,
                email = @email,
                userName = @userName,
                password = @password,
                userType = @userType,
                userRole = @userRole,
                arrears = @arrears,
                isActive = @isActive
            WHERE document = @document";

        public const string ReactivateUserByDocument = @"
            UPDATE Users SET isActive = 1
            WHERE document = @document";

        public const string DeactivateUserByDocument = @"
            UPDATE Users SET isActive = 0
            WHERE document = @document";

        public const string UpdateUserPassword = @"
            UPDATE Users SET password = @password
            WHERE userName = @username";

        public const string UpdateUserArrears = @"
            UPDATE Users SET
                Arrears = @arrears,
                IsActive = @isActive
            WHERE Id = @userId";
    }
}
