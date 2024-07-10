class JWTStorage {
  constructor(accessToken = 'access_token', refreshToken = 'refresh_token') {
      this.accessToken = accessToken;
      this.refreshToken = refreshToken;
  }

 // Save the access token to local storage
  saveAccessToken(token) {
    localStorage.setItem(this.accessToken, token);
  }

  // Retrieve the access token from local storage
  getAccessToken() {
    return localStorage.getItem(this.accessToken);
  }

  // Delete the access token from local storage
  removeAccessToken() {
    localStorage.removeItem(this.accessToken);
  }

  // Save the refresh token to local storage
  saveRefreshToken(token) {
    localStorage.setItem(this.refreshToken, token);
  }

  // Retrieve the refresh token from local storage
  getRefreshToken() {
    return localStorage.getItem(this.refreshToken);
  }

  // Delete the refresh token from local storage
  removeRefreshToken() {
    localStorage.removeItem(this.refreshToken);
  }

  // Check if the access token exists in local storage
  hasAccessToken() {
    return !!this.getAccessToken();
  }

  // Check if the refresh token exists in local storage
  hasRefreshToken() {
    return !!this.getRefreshToken();
  }
}