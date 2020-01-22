module.exports = (router) => {
    
    router.get("/movies", (req, res) => {
        res.send({'msg':'Movies!'})
    });

    return router
}