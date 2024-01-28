$(document).ready(function () {
    let currentPage = 1;
    let pageSize = 10;
    let contentType = 'Posts';
    let searchParameters = {};

    // Load the default content on page load
    loadContent(currentPage, pageSize, contentType, searchParameters);

    // Handler for changing search type
    $('.search-dropdown-item').click(function () {
        contentType = $(this).data('value');
        $('.search-dropdown-toggle').text(contentType);
        loadContent(1, pageSize, contentType, searchParameters);  // reset to page 1 on type change
    });

    // Handler for pressing the Enter key in the search input field
    $('.search-input').keypress(function (event) {
        let keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode === '13') {
            event.preventDefault();
            searchParameters = {};  // reset search parameters
            searchParameters.username = $(this).val();  // set username for user search
            searchParameters.tags = $(this).val().split(',');  // set tags for tag search
            loadContent(1, pageSize, contentType, searchParameters);  // reset to page 1 on new search
        }
    });

    // Handler for clicking the Next button
    $("#nextButton").click(function () {
        checkNext(currentPage, pageSize, contentType, searchParameters).done(function (data) {
            if (data) {
                currentPage += 1;
                loadContent(currentPage, pageSize, contentType, searchParameters);
            }
        });
    });

    // Handler for clicking the Previous button
    $("#prevButton").click(function () {
        if (currentPage > 1) {
            currentPage -= 1;
            loadContent(currentPage, pageSize, contentType, searchParameters);
        }
    });

    // Handler for changing page size
    $("#pageSizeDropdown").change(function () {
        pageSize = $(this).val();
        loadContent(currentPage, pageSize, contentType, searchParameters);
    });

    // Handler for clicking the Search button
    $("#searchButton").click(function () {
        searchParameters = {};  // reset search parameters
        searchParameters.username = $(".search-input").val();  // set username for user search
        searchParameters.tags = $(".search-input").val().split(',');  // set tags for tag search
        loadContent(1, pageSize, contentType, searchParameters);  // reset to page 1 on new search
    });
});

// Function to load content
function loadContent(page, size, type, parameters) {
    let url;
    let checkUrl;
    if (type === 'Posts') {
        url = '@Url.Action("GetPostsPartial", "Home")';
        checkUrl = '@Url.Action("CheckNext", "Home")';
    } else if (type === 'Users') {
        url = '@Url.Action("GetUsersPartial", "Home")';
        checkUrl = '@Url.Action("CheckNext", "Home")';
    } else if (type === 'Tags') {
        url = '@Url.Action("GetPostsByTagsPartial", "Home")';
        checkUrl = '@Url.Action("CheckNext", "Home")';
    }

    let params = {
        page: page,
        size: size,
        ...parameters
    };

    // Update main content
    url += '?' + $.param(params);
    $('#mainContent').load(url);

    // Update next button
    checkNext(page, size, type, parameters).done(function (data) {
        $("#nextButton").prop("disabled", !data);
    });

    // Update previous button
    $("#prevButton").prop("disabled", page <= 1);
}

// Function to check if there is a next page
function checkNext(page, size, type, parameters) {
    let url;
    if (type === 'Posts') {
        url = '@Url.Action("CheckNext", "Home")';
    } else if (type === 'Users') {
        url = '@Url.Action("CheckNext", "Home")';
    }

    let params = {
        page: page,
        size: size,
        ...parameters
    };

    url += '?' + $.param(params);
    return $.getJSON(url);
}
