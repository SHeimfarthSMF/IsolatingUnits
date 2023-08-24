namespace IsolatingUnits;

public interface IDbContext
{
    Customer CreateCustomer(Guid id, string firstName, string lastName, string email);
    Customer? GetCustomerById(Guid id);
    bool DeleteCustomer(Guid id);
}