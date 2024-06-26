$(document).ready(function () {
    console.log('Document ready');
    const jwtStorage = new JWTStorage();

    $('#loginForm').submit(function (event) {
        console.log('Form submitted');
        event.preventDefault();
        $.ajax({
            url: $(this).attr('action'),
            type: 'POST',
            data: $(this).serialize(),
            success: function (response) {
                console.log('AJAX success', response);
                if (response.success) {
                    jwtStorage.saveAccessToken(response.accessToken);
                    jwtStorage.saveRefreshToken(response.refreshToken);
                    window.location.href = '/';

                }
                else {
                    alert(response.Detail);
                }
            },
            error: function (xhr, status, error) {
                console.error('AJAX error', status, error);
                alert('An error occurred. Please try again.');
            }
        })
    })
})