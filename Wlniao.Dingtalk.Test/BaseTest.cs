using NUnit.Framework;

namespace Wlniao.Dingtalk.Test
{
    public class BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAppToken()
        {
            var res = new Context(new Dictionary<string, string>
            {
                {"AppKey","dingcupqf4k5ojlrf0tq" },
                {"AppSecret","a8dK3uZLrw65aXM9xurpcyN1CRohPtTiPqrU4Zo3U-Qe57BWkSkMKV9-a4LUMGmS" }
            }).GetAppToken();
            Assert.Pass();
        }
    }
}