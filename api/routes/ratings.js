const ratingsController = require('./../controllers/ratings')

module.exports = (router, validator, error) => {

    router.get("/users/:cpUserId/ratings", (req, res) => {
        ratingsController.findAllByCpUserId(req, res, error(res))
    })

    return router
}