var router = require('express').Router()
var users = require('./users')
var movies = require('./movies')

router.get("/", (req, res) => {
    res.send({'msg':'API do CP Scraper'})
});

router = users(router)
router = movies(router)

module.exports = router