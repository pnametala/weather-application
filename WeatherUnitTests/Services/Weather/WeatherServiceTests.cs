using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using WeatherApplication.OpenWeather.Models.OneCall;
using WeatherApplication.OpenWeather.Services.Cities;
using WeatherApplication.OpenWeather.Services.Weather;
using WeatherApplication.Settings;

namespace WeatherUnitTests.Services.Weather
{
    public class WeatherServiceTests
    {
        private string _baseAddress = "http://anything.com";
        private IWeatherService _service { get; set; }
        private Mock<HttpMessageHandler> _handler { get; set; }
        [OneTimeSetUp]
        public void Setup()
        {
            _service = new WeatherService(new OpenWeatherApiSettings
            {
                ApiKey = string.Empty,
                BaseUrl = _baseAddress
            });
            _handler = new Mock<HttpMessageHandler>();
        }

        [Test(Description = "When calling search history with no previous searches, should obtain an empty list")]
        public void OneCall_WithUnauthorized_ShouldThrowException()
        {
            var code = HttpStatusCode.Unauthorized;
            var msg = "No API Key provided";
            SetupOneCallMessage(code, msg);
            var ex = Assert.ThrowsAsync<Exception>(() => _service.OneCall(double.Epsilon, double.Epsilon));
            
            Assert.That(ex.Message, Is.EqualTo($"{code} - {msg}"));
        }
        
        [Test(Description = "When calling search history with no previous searches, should obtain an empty list")]
        public async Task OneCall_WithOkAndValidContent_ShouldGetData()
        {
            var code = HttpStatusCode.OK;
            var msg = "No API Key provided";
            SetupOneCallMessage(code, string.Empty, ValidResponse);
            var forecast = await _service.OneCall(double.Epsilon, double.Epsilon);
            
            Assert.That(forecast.GetType(), Is.EqualTo(typeof(OneCallData)));
        }
        
        private void SetupOneCallMessage(HttpStatusCode code, string msg, string content = "")
        {
            _service.SetHttpClient(GetMockHttpClient(_baseAddress));
            _handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = code,
                    ReasonPhrase = msg,
                    Content = new StringContent(content)
                });
        }


        private HttpClient GetMockHttpClient(string baseAddress)
        {
            var client = new HttpClient(_handler.Object);
            client.BaseAddress = new Uri(baseAddress);
            return client;
        }
        
        private const string ValidResponse = "{'lat':-31.93,'lon':115.83,'timezone':'Australia/Perth','timezone_offset':28800,'current':{'dt':1596448956,'sunrise':1596409433,'sunset':1596447706,'temp':10,'feels_like':2.9,'pressure':1005,'humidity':93,'dew_point':8.92,'uvi':3.14,'clouds':75,'visibility':10000,'wind_speed':9.8,'wind_deg':50,'weather':[{'id':803,'main':'Clouds','description':'broken clouds','icon':'04n'}]},'hourly':[{'dt':1596448800,'temp':10,'feels_like':6,'pressure':1005,'humidity':93,'dew_point':8.92,'clouds':75,'visibility':10000,'wind_speed':5.37,'wind_deg':22,'weather':[{'id':500,'main':'Rain','description':'light rain','icon':'10n'}],'pop':1,'rain':{'1h':0.42}},{'dt':1596452400,'temp':10.7,'feels_like':7.39,'pressure':1006,'humidity':84,'dew_point':8.11,'clouds':80,'visibility':10000,'wind_speed':4.1,'wind_deg':60,'weather':[{'id':500,'main':'Rain','description':'light rain','icon':'10n'}],'pop':1,'rain':{'1h':0.17}},{'dt':1596456000,'temp':10.86,'feels_like':7.33,'pressure':1007,'humidity':78,'dew_point':7.18,'clouds':76,'visibility':10000,'wind_speed':4.1,'wind_deg':94,'weather':[{'id':803,'main':'Clouds','description':'broken clouds','icon':'04n'}],'pop':0.8},{'dt':1596459600,'temp':10.89,'feels_like':7.44,'pressure':1008,'humidity':76,'dew_point':6.83,'clouds':12,'visibility':10000,'wind_speed':3.87,'wind_deg':104,'weather':[{'id':801,'main':'Clouds','description':'few clouds','icon':'02n'}],'pop':0.25},{'dt':1596463200,'temp':10.76,'feels_like':6.89,'pressure':1008,'humidity':75,'dew_point':6.51,'clouds':8,'visibility':10000,'wind_speed':4.37,'wind_deg':102,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0.19},{'dt':1596466800,'temp':10.27,'feels_like':7.64,'pressure':1008,'humidity':76,'dew_point':6.34,'clouds':5,'visibility':10000,'wind_speed':2.51,'wind_deg':118,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0.19},{'dt':1596470400,'temp':9.99,'feels_like':7,'pressure':1008,'humidity':77,'dew_point':6.25,'clouds':3,'visibility':10000,'wind_speed':3,'wind_deg':124,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0.11},{'dt':1596474000,'temp':9.73,'feels_like':6.86,'pressure':1009,'humidity':77,'dew_point':5.99,'clouds':5,'visibility':10000,'wind_speed':2.76,'wind_deg':123,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0.09},{'dt':1596477600,'temp':9.53,'feels_like':6.98,'pressure':1009,'humidity':77,'dew_point':5.73,'clouds':4,'visibility':10000,'wind_speed':2.24,'wind_deg':155,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0.09},{'dt':1596481200,'temp':9.39,'feels_like':5.71,'pressure':1009,'humidity':79,'dew_point':6.02,'clouds':0,'visibility':10000,'wind_speed':3.92,'wind_deg':157,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0},{'dt':1596484800,'temp':9.1,'feels_like':5.88,'pressure':1009,'humidity':78,'dew_point':5.51,'clouds':0,'visibility':10000,'wind_speed':3.13,'wind_deg':150,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0},{'dt':1596488400,'temp':9.22,'feels_like':5.36,'pressure':1009,'humidity':77,'dew_point':5.49,'clouds':12,'visibility':10000,'wind_speed':4.03,'wind_deg':167,'weather':[{'id':801,'main':'Clouds','description':'few clouds','icon':'02n'}],'pop':0},{'dt':1596492000,'temp':9.92,'feels_like':5.88,'pressure':1010,'humidity':76,'dew_point':6.02,'clouds':28,'visibility':10000,'wind_speed':4.43,'wind_deg':181,'weather':[{'id':802,'main':'Clouds','description':'scattered clouds','icon':'03n'}],'pop':0},{'dt':1596495600,'temp':10.38,'feels_like':6.12,'pressure':1010,'humidity':80,'dew_point':7.2,'clouds':37,'visibility':10000,'wind_speed':5.12,'wind_deg':173,'weather':[{'id':802,'main':'Clouds','description':'scattered clouds','icon':'03n'}],'pop':0},{'dt':1596499200,'temp':11.38,'feels_like':5.97,'pressure':1011,'humidity':80,'dew_point':8.17,'clouds':46,'visibility':10000,'wind_speed':7.08,'wind_deg':170,'weather':[{'id':802,'main':'Clouds','description':'scattered clouds','icon':'03d'}],'pop':0},{'dt':1596502800,'temp':12.59,'feels_like':6.59,'pressure':1012,'humidity':77,'dew_point':8.81,'clouds':99,'visibility':10000,'wind_speed':8.14,'wind_deg':169,'weather':[{'id':804,'main':'Clouds','description':'overcast clouds','icon':'04d'}],'pop':0.26},{'dt':1596506400,'temp':13.85,'feels_like':7.55,'pressure':1012,'humidity':72,'dew_point':9.07,'clouds':83,'visibility':10000,'wind_speed':8.65,'wind_deg':166,'weather':[{'id':803,'main':'Clouds','description':'broken clouds','icon':'04d'}],'pop':0.18},{'dt':1596510000,'temp':14.02,'feels_like':8.29,'pressure':1012,'humidity':72,'dew_point':9.1,'clouds':89,'visibility':10000,'wind_speed':7.89,'wind_deg':166,'weather':[{'id':804,'main':'Clouds','description':'overcast clouds','icon':'04d'}],'pop':0.07},{'dt':1596513600,'temp':14.36,'feels_like':8.83,'pressure':1012,'humidity':70,'dew_point':9.14,'clouds':92,'visibility':10000,'wind_speed':7.57,'wind_deg':167,'weather':[{'id':804,'main':'Clouds','description':'overcast clouds','icon':'04d'}],'pop':0.05},{'dt':1596517200,'temp':15.31,'feels_like':9.55,'pressure':1011,'humidity':65,'dew_point':8.83,'clouds':83,'visibility':10000,'wind_speed':7.83,'wind_deg':169,'weather':[{'id':803,'main':'Clouds','description':'broken clouds','icon':'04d'}],'pop':0.09},{'dt':1596520800,'temp':16.23,'feels_like':10.04,'pressure':1011,'humidity':59,'dew_point':8.3,'clouds':73,'visibility':10000,'wind_speed':8.25,'wind_deg':173,'weather':[{'id':803,'main':'Clouds','description':'broken clouds','icon':'04d'}],'pop':0.09},{'dt':1596524400,'temp':16.45,'feels_like':10.62,'pressure':1011,'humidity':58,'dew_point':8.27,'clouds':58,'visibility':10000,'wind_speed':7.72,'wind_deg':180,'weather':[{'id':803,'main':'Clouds','description':'broken clouds','icon':'04d'}],'pop':0.1},{'dt':1596528000,'temp':16.18,'feels_like':10.55,'pressure':1012,'humidity':61,'dew_point':8.73,'clouds':61,'visibility':10000,'wind_speed':7.6,'wind_deg':174,'weather':[{'id':803,'main':'Clouds','description':'broken clouds','icon':'04d'}],'pop':0.09},{'dt':1596531600,'temp':15.26,'feels_like':9.65,'pressure':1013,'humidity':64,'dew_point':8.51,'clouds':56,'visibility':10000,'wind_speed':7.52,'wind_deg':171,'weather':[{'id':803,'main':'Clouds','description':'broken clouds','icon':'04d'}],'pop':0.07},{'dt':1596535200,'temp':14.26,'feels_like':8.62,'pressure':1014,'humidity':67,'dew_point':8.29,'clouds':54,'visibility':10000,'wind_speed':7.47,'wind_deg':171,'weather':[{'id':803,'main':'Clouds','description':'broken clouds','icon':'04n'}],'pop':0.1},{'dt':1596538800,'temp':13.84,'feels_like':7.67,'pressure':1015,'humidity':67,'dew_point':7.99,'clouds':47,'visibility':10000,'wind_speed':8.08,'wind_deg':171,'weather':[{'id':802,'main':'Clouds','description':'scattered clouds','icon':'03n'}],'pop':0.04},{'dt':1596542400,'temp':13.54,'feels_like':7.23,'pressure':1015,'humidity':69,'dew_point':8.13,'clouds':44,'visibility':10000,'wind_speed':8.33,'wind_deg':170,'weather':[{'id':802,'main':'Clouds','description':'scattered clouds','icon':'03n'}],'pop':0.03},{'dt':1596546000,'temp':13.19,'feels_like':7.02,'pressure':1016,'humidity':71,'dew_point':8.14,'clouds':13,'visibility':10000,'wind_speed':8.16,'wind_deg':170,'weather':[{'id':801,'main':'Clouds','description':'few clouds','icon':'02n'}],'pop':0},{'dt':1596549600,'temp':12.94,'feels_like':7,'pressure':1017,'humidity':72,'dew_point':8.15,'clouds':6,'visibility':10000,'wind_speed':7.83,'wind_deg':168,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0},{'dt':1596553200,'temp':12.82,'feels_like':7.14,'pressure':1017,'humidity':73,'dew_point':8.16,'clouds':4,'visibility':10000,'wind_speed':7.49,'wind_deg':168,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0},{'dt':1596556800,'temp':12.69,'feels_like':7.21,'pressure':1017,'humidity':73,'dew_point':8.01,'clouds':3,'visibility':10000,'wind_speed':7.16,'wind_deg':168,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0},{'dt':1596560400,'temp':12.64,'feels_like':7.15,'pressure':1017,'humidity':72,'dew_point':7.89,'clouds':3,'visibility':10000,'wind_speed':7.09,'wind_deg':169,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0},{'dt':1596564000,'temp':12.65,'feels_like':7.26,'pressure':1017,'humidity':73,'dew_point':8.04,'clouds':6,'visibility':10000,'wind_speed':7.02,'wind_deg':171,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0},{'dt':1596567600,'temp':12.44,'feels_like':7.24,'pressure':1018,'humidity':74,'dew_point':8.13,'clouds':8,'visibility':10000,'wind_speed':6.74,'wind_deg':168,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0},{'dt':1596571200,'temp':12.26,'feels_like':7.05,'pressure':1018,'humidity':76,'dew_point':8.17,'clouds':14,'visibility':10000,'wind_speed':6.83,'wind_deg':163,'weather':[{'id':801,'main':'Clouds','description':'few clouds','icon':'02n'}],'pop':0},{'dt':1596574800,'temp':12.07,'feels_like':7.02,'pressure':1018,'humidity':77,'dew_point':8.24,'clouds':11,'visibility':10000,'wind_speed':6.61,'wind_deg':170,'weather':[{'id':801,'main':'Clouds','description':'few clouds','icon':'02n'}],'pop':0},{'dt':1596578400,'temp':11.92,'feels_like':6.71,'pressure':1019,'humidity':78,'dew_point':8.23,'clouds':8,'visibility':10000,'wind_speed':6.85,'wind_deg':169,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0},{'dt':1596582000,'temp':11.78,'feels_like':6.86,'pressure':1019,'humidity':79,'dew_point':8.28,'clouds':6,'visibility':10000,'wind_speed':6.45,'wind_deg':167,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'pop':0},{'dt':1596585600,'temp':12.16,'feels_like':7.39,'pressure':1020,'humidity':78,'dew_point':8.5,'clouds':5,'visibility':10000,'wind_speed':6.3,'wind_deg':168,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01d'}],'pop':0},{'dt':1596589200,'temp':13.52,'feels_like':8.85,'pressure':1021,'humidity':72,'dew_point':8.78,'clouds':0,'visibility':10000,'wind_speed':6.2,'wind_deg':167,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01d'}],'pop':0},{'dt':1596592800,'temp':14.81,'feels_like':10.56,'pressure':1021,'humidity':67,'dew_point':8.81,'clouds':2,'visibility':10000,'wind_speed':5.67,'wind_deg':160,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01d'}],'pop':0},{'dt':1596596400,'temp':15.94,'feels_like':11.81,'pressure':1021,'humidity':63,'dew_point':8.92,'clouds':16,'visibility':10000,'wind_speed':5.56,'wind_deg':160,'weather':[{'id':801,'main':'Clouds','description':'few clouds','icon':'02d'}],'pop':0},{'dt':1596600000,'temp':16.78,'feels_like':12.59,'pressure':1021,'humidity':58,'dew_point':8.66,'clouds':28,'visibility':10000,'wind_speed':5.48,'wind_deg':158,'weather':[{'id':802,'main':'Clouds','description':'scattered clouds','icon':'03d'}],'pop':0},{'dt':1596603600,'temp':17.43,'feels_like':13.35,'pressure':1020,'humidity':55,'dew_point':8.5,'clouds':25,'visibility':10000,'wind_speed':5.27,'wind_deg':161,'weather':[{'id':802,'main':'Clouds','description':'scattered clouds','icon':'03d'}],'pop':0},{'dt':1596607200,'temp':17.87,'feels_like':13.6,'pressure':1020,'humidity':54,'dew_point':8.65,'clouds':21,'visibility':10000,'wind_speed':5.58,'wind_deg':166,'weather':[{'id':801,'main':'Clouds','description':'few clouds','icon':'02d'}],'pop':0},{'dt':1596610800,'temp':17.86,'feels_like':13.53,'pressure':1020,'humidity':56,'dew_point':9.02,'clouds':16,'visibility':10000,'wind_speed':5.86,'wind_deg':167,'weather':[{'id':801,'main':'Clouds','description':'few clouds','icon':'02d'}],'pop':0},{'dt':1596614400,'temp':17.23,'feels_like':12.74,'pressure':1021,'humidity':57,'dew_point':8.9,'clouds':38,'visibility':10000,'wind_speed':5.97,'wind_deg':148,'weather':[{'id':802,'main':'Clouds','description':'scattered clouds','icon':'03d'}],'pop':0},{'dt':1596618000,'temp':16.23,'feels_like':11.96,'pressure':1022,'humidity':59,'dew_point':8.37,'clouds':32,'visibility':10000,'wind_speed':5.51,'wind_deg':144,'weather':[{'id':802,'main':'Clouds','description':'scattered clouds','icon':'03d'}],'pop':0}],'daily':[{'dt':1596427200,'sunrise':1596409433,'sunset':1596447706,'temp':{'day':10,'min':10,'max':10.54,'night':10.21,'eve':10,'morn':10},'feels_like':{'day':7.12,'night':7.69,'eve':7.12,'morn':7.12},'pressure':1005,'humidity':93,'dew_point':8.92,'wind_speed':3.78,'wind_deg':3,'weather':[{'id':500,'main':'Rain','description':'light rain','icon':'10d'}],'clouds':75,'pop':1,'rain':2.68,'uvi':3.14},{'dt':1596513600,'sunrise':1596495785,'sunset':1596534145,'temp':{'day':14.02,'min':9.23,'max':16.23,'night':12.82,'eve':15.26,'morn':9.23},'feels_like':{'day':8.29,'night':7.14,'eve':9.65,'morn':5.37},'pressure':1012,'humidity':72,'dew_point':9.1,'wind_speed':7.89,'wind_deg':166,'weather':[{'id':500,'main':'Rain','description':'light rain','icon':'10d'}],'clouds':89,'pop':0.2,'rain':0.1,'uvi':3.75},{'dt':1596600000,'sunrise':1596582135,'sunset':1596620584,'temp':{'day':15.94,'min':12.07,'max':17.87,'night':12.18,'eve':16.23,'morn':12.07},'feels_like':{'day':11.81,'night':8.31,'eve':11.96,'morn':7.02},'pressure':1021,'humidity':63,'dew_point':8.92,'wind_speed':5.56,'wind_deg':160,'weather':[{'id':801,'main':'Clouds','description':'few clouds','icon':'02d'}],'clouds':16,'pop':0,'uvi':3.77},{'dt':1596686400,'sunrise':1596668485,'sunset':1596707024,'temp':{'day':14.19,'min':9.96,'max':16.77,'night':12.17,'eve':16.04,'morn':10.05},'feels_like':{'day':10.12,'night':7.39,'eve':12.36,'morn':6.46},'pressure':1027,'humidity':54,'dew_point':5.28,'wind_speed':4.21,'wind_deg':99,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01d'}],'clouds':0,'pop':0,'uvi':3.71},{'dt':1596772800,'sunrise':1596754833,'sunset':1596793463,'temp':{'day':14.14,'min':10.03,'max':16.14,'night':12.57,'eve':15.41,'morn':10.03},'feels_like':{'day':8.75,'night':6.28,'eve':10.74,'morn':5.72},'pressure':1025,'humidity':54,'dew_point':5.19,'wind_speed':6.08,'wind_deg':68,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01d'}],'clouds':1,'pop':0,'uvi':3.61},{'dt':1596859200,'sunrise':1596841180,'sunset':1596879902,'temp':{'day':14.68,'min':10.9,'max':17.5,'night':16.09,'eve':17.05,'morn':10.97},'feels_like':{'day':7.39,'night':8.62,'eve':11.01,'morn':3.37},'pressure':1020,'humidity':50,'dew_point':4.45,'wind_speed':8.63,'wind_deg':59,'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01d'}],'clouds':0,'pop':0,'uvi':3.93},{'dt':1596945600,'sunrise':1596927526,'sunset':1596966341,'temp':{'day':12.57,'min':12.52,'max':14.49,'night':14.49,'eve':13.48,'morn':12.65},'feels_like':{'day':3.5,'night':8.57,'eve':3.6,'morn':5.87},'pressure':1011,'humidity':64,'dew_point':6.08,'wind_speed':11.63,'wind_deg':53,'weather':[{'id':501,'main':'Rain','description':'moderate rain','icon':'10d'}],'clouds':100,'pop':1,'rain':10.53,'uvi':3.39},{'dt':1597032000,'sunrise':1597013871,'sunset':1597052781,'temp':{'day':18.26,'min':15.22,'max':18.47,'night':17.44,'eve':17.58,'morn':15.22},'feels_like':{'day':13.54,'night':11.56,'eve':11.27,'morn':10.73},'pressure':1000,'humidity':69,'dew_point':12.56,'wind_speed':7.84,'wind_deg':307,'weather':[{'id':502,'main':'Rain','description':'heavy intensity rain','icon':'10d'}],'clouds':97,'pop':1,'rain':21.18,'uvi':4.02}]}";
    }
}