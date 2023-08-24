namespace IsolatingUnits;

public class DbContext
{
    private readonly List<Customer> _customers = new()
    {
        new()
        {
            Id = Guid.Parse("9ed58d64-e654-472b-9001-c9da00f081cf"),
            FirstName = "John",
            LastName = "Doe",
            Email = "John.Doe@example.com"
        },
        new ()
        {
            Id = Guid.Parse("41a71355-c3f7-47e6-a7d9-6020b1b77d60"),
            FirstName = "Jane",
            LastName = "Doe",
            Email = "Jane.Doe@example.com"
        },
        new ()
        {
            Id = Guid.Parse("09a83fb2-ae19-4899-903e-3f6952ccd882"),
            FirstName = "John",
            LastName = "Smith",
            Email = "Jon.Smith@example.com"
        },
        new ()
        {
            Id = Guid.Parse("1a01774d-9c9a-4e19-8ba3-3ce4a7c132bc"),
            FirstName = "Jane",
            LastName = "Smith",
            Email = "Jane.Smith@example.com"
        }
    };

    public Customer CreateCustomer(Guid id, string firstName, string lastName, string email)
    {
        var customer = new Customer
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        _customers.Add(customer);
        return customer;
    }

    public Customer? GetCustomerById(Guid id)
    {
        return _customers.FirstOrDefault(c => c.Id == id);
    }

    public bool DeleteCustomer(Guid id)
    {
        var customer = GetCustomerById(id);
        if (customer is null)
        {
            return false;
        }

        _customers.Remove(customer);
        return true;
    }
}