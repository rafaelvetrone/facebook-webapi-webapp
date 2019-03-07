var accessToken = 'EAAFbcj4QDScBAL56RwZBbdi48dJ0DbSzqQpnXuZCDXGT1Se4z3i2q7mYjZB168ghq1oOoHNRDbNaI2A3VKvoJAWxHZCOcbi6XrkSoLCzxqmwCTRZAbVNVjEhyx0cZBEBNM5asO3aMwpSgvjmERnaa9X0vATtp9VoPZAyqU6AtuGqL6e8j66aHoK5SiYpHcQCRIZD';

function like(id)
{
    //window.alert("DONE!");
    FB.api(
    "/" + id + "/likes",
    "POST",    
    function (response) {
        if (response && !response.error) {
            window.alert("DONE!");
        }
    });
}

function unlike(id)
{
    //window.alert("UNDONE!");
    FB.api(
    "/" + id + "/likes",
    "DELETE",    
    function (response) {
        if (response && !response.error) {
            window.alert("UNDONE!");
        }
    });
}