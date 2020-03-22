using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cian
{
    /// <summary>
    ///     Иззображение с циана
    /// </summary>
    public static class CianImage
    {
        #region Fields

        private const int _downloadImageTimeoutInSeconds = 3;
        private static readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(_downloadImageTimeoutInSeconds) };

        #endregion

        #region Public Methods

        /// <summary>
        ///     Загрузить картинку по ссылке
        /// </summary>
        /// <param name="url">
        ///     Ссылка на картинку
        /// </param>
        public static async Task<ImageSource> LoadAsync(string url)
        {
            var bytes = await DownloadImageAsync(url);
            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Скачать байтовый массив изоображения
        /// </summary>
        /// <param name="imageUrl">
        ///     Ссылка на картинку
        /// </param>
        /// <returns>
        ///     Байтовый массив
        /// </returns>
        private static async Task<byte[]> DownloadImageAsync(string imageUrl)
        {
            try
            {
                using (var httpResponse = await _httpClient.GetAsync(imageUrl))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return await httpResponse.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion
    }

}
