using Xunit;
using Jellyfin.Plugin.RemoteServerSync.Helpers;
using Jellyfin.Plugin.RemoteServerSync.Models;
using Moq;

namespace Jellyfin.Plugin.RemoteServerSync.Tests
{
    public class RemoteServerSyncTests
    {
        private readonly Mock<IUser Manager> _userManagerMock;
        private readonly Mock<ILibraryManager> _libraryManagerMock;
        private readonly ServerSyncHelper _syncHelper;

        public RemoteServerSyncTests()
        {
            _userManagerMock = new Mock<IUser Manager>();
            _libraryManagerMock = new Mock<ILibraryManager>();
            _syncHelper = new ServerSyncHelper(_userManagerMock.Object, _libraryManagerMock.Object);
        }

        [Fact]
        public async Task SyncUsersAsync_ShouldSyncUsers()
        {
            // Arrange
            var server = new RemoteServer { Url = "http://example.com", ApiKey = "test-api-key" };
            _userManagerMock.Setup(m => m.Users).Returns(new List<User> { new User { Name = "Test User" } });

            // Act
            await _syncHelper.SyncUsersAsync(server);

            // Assert
            // Verify that the users were synced correctly
        }
    }
}