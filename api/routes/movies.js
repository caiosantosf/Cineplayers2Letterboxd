module.exports = (router, validator, error) => {
    
    router.get("/movies", (req, res) => {
        res.send({'msg':'Movies!'})
    });

    return router
}