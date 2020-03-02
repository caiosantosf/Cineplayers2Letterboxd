const watchlistsController = require('./../controllers/watchlists')

module.exports = (router, validator, error) => {

    router.get("/users/:cpUserId/watchlists", (req, res) => {
        watchlistsController.findAllByCpUserId(req, res, error(res))
    })

    router.delete("/users/:cpUserId/watchlists/:id", (req, res) => {
        watchlistsController.deleteAllByCpUserId(req, res, error(res))
    })

    return router
}