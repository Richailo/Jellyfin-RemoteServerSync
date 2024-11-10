using System.Net.Http.Json;
using System.Text.Json;
using Jellyfin.Plugin.RemoteServerSync.Models;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Users;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.RemoteServerSync.Helpers
{
    public class ServerSyncHelper
    {
        private readonly IUserManager _userManager;
        private readonly ILibraryManager _libraryManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ServerSyncHelper> _logger;

        public ServerSyncHelper(
            IUserManager userManager,
            ILibraryManager libraryManager,
            IHttpClientFactory httpClientFactory,
            ILogger<ServerSyncHelper> logger)
        {
            _userManager = userManager;
            _libraryManager = libraryManager;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task SyncUsersAsync(RemoteServer server)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", server.ApiKey);

                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    var userDto = new 
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email
                    };

                    await httpClient.PostAsJsonAsync($"{server.Url}/Users", userDto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User synchronization failed");
            }
        }

        public async Task<List<MediaItemDto>> GetRemoteMediaAsync(RemoteServer server, string libraryName)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", server.ApiKey);

                var response = await httpClient.GetAsync($"{server.Url}/Libraries/{libraryName}/Items");
                response.EnsureSuccessStatusCode();

                var mediaItems = await response.Content.ReadFromJsonAsync<List<MediaItemDto>>();
                return mediaItems ?? new List<MediaItemDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Media retrieval failed");
                return new List<MediaItemDto>();
            }
        }
    }
}