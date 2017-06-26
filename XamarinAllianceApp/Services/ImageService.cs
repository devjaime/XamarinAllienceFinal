using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinAllianceApp.Helpers;

namespace XamarinAllianceApp.Services
{
    public static class ImageService
    {
        public static async Task<Stream> GetImage()
        {
            try
            {
                var client = new MobileServiceClient(Constants.MobileServiceClientUrl);
                var token = await client.InvokeApiAsync<String>("/api/StorageToken/CreateToken");
                string storageAccountName = "xamarinalliance";//cuenta de almacenamiento que contiene el blog
                StorageCredentials credentials = new StorageCredentials(token);
                CloudStorageAccount account = new CloudStorageAccount(credentials, storageAccountName, null, true);
                var blobclient = account.CreateCloudBlobClient();
                var container = blobclient.GetContainerReference("images");
                var blog = container.GetBlobReference("XAMARIN-Alliance-logo.png");

                MemoryStream stream = new MemoryStream();
                await blog.DownloadToStreamAsync(stream);
                return stream;

            }
            catch(Exception)
            {
                throw;
            }
        }
        public static async Task<string> GetGUID()
        { 
            
            var client = new MobileServiceClient(Constants.MobileServiceClientUrl);
            var guid = await client.InvokeApiAsync<string>("/api/XamarinAlliance/ReceiveCredit");
            return guid;
        }
    }
}

