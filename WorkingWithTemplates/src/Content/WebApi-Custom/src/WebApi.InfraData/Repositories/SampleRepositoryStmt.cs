namespace WebApi.InfraData.Repositories
{
    public static class SampleRepositoryStmt
    {
        public const string Insert = @"
            insert into SAMPLE_TABLE (
                testproperty, 
                active
            ) values(
                @TestProperty, 
                @Active
            )";

        public const string Update = @"
            update SAMPLE_TABLE set 
                testproperty = @TestProperty, 
                active = @Active 
            where id = @id";

        public const string Remove = @"
            delete from SAMPLE_TABLE 
            where id = @id";

        public const string GetById = @"
            select  id, 
                    testproperty, 
                    active 
            from SAMPLE_TABLE 
            where id = @id";
    }
}