
namespace WebShop.Domain.Exceptions;

public class NotFoundException(string resType,string resIdentitifier) 
    : Exception($"{resType} with id: {resIdentitifier} does not exist in application!")
{
}
