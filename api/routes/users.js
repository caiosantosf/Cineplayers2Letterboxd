const usersController = require('./../controllers/users')

module.exports = (router, validator, error) => {

    const { check, validationResult } = validator
    
    router.get("/users/:id", (req, res) => usersController.findOne(req, res, error(res)))
    
    router.post("/users", [
        check('email').isEmail().normalizeEmail(),
        check('cpUserId').isNumeric()
      ], 
      (req, res) => {
        const reqErrors = validationResult(req);
        if (!reqErrors.isEmpty()) {
          return res.status(422).json({ errors: reqErrors.array() });
        }

        usersController.create(req, res, error(res))
    })

    router.delete("/users/:id", (req, res) => usersController.delete(req, res, error(res)))

    return router
}