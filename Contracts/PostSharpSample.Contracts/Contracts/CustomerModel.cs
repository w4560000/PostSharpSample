

using PostSharp.Patterns.Contracts;

namespace PostSharpSample.Contracts.Contracts
{
    public interface ICustomerModel
    {
        void SetFullName([Required] string firstName, [Required] string lastName);
    }

    public class CustomerModel : ICustomerModel
    {
        public string FullName { get; set; }

        public void SetFullName(string firstName, string lastName)
        {
            FullName = firstName + " " + lastName;
        }
    }
}