using Microsoft.VisualStudio.TestPlatform.TestHost;
using Examen;
namespace Examen.Test
{
    public class UnitTest1
    {
        [Fact]
        public void TearDown() // ������� ��������� �����
        {
           
            if (File.Exists("distances.csv"))
            {
                File.Delete("distances.csv");
            }
        }
    }
}