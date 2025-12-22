using dirtbike.api.Models;
using dirtbike.api.Data;
namespace Enterpriseservices
{
    
public static class TwoFactorHelper
{

    public static int GenerateSixDigitRandom()
    {
                Random rnd = new Random();
                int dice = rnd.Next(100000, 1000000);
                return dice;
    }

}
}
