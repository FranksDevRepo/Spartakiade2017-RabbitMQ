using EasyNetQ;

namespace FP.Spartakiade2017.PicFlow.Php2Net
{
    [Queue("samplequeue", ExchangeName = "php2net")]
    public class DtoTest
    {
        public string Text { get; set; }
    }
}
