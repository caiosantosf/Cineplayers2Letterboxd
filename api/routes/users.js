const usersController = require('./../controllers/users')

module.exports = (router) => {
    
    router.get("/users", (req, res) => {
        res.send({'msg':'Users!'})
    });
    
    router.post("/users", usersController.create)
    //router.post("/users/:id/watchlist", usersController.createWatchlist)

    return router
}