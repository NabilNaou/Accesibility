class ApiService {

    constructor(baseURL) {
        this.baseURL = baseURL || "";
    }

    likePost(postId) {
        return fetch("/PublicPost/PostLike/" + postId,
            {
                method: "POST"
            })
            .then(response => response.json())
    }

    reportPost(postId) {
        return fetch("/PublicPost/PostReport/" + postId,
            {
                method: "POST"
            })
            .then(response => response.json())
    }

}

var apiService = new ApiService();