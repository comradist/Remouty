using System.Net.Http;

namespace OutOfOffice.MVC.Services.Base;

public partial interface IClient
{
    public HttpClient HttpClient { get; }

}

