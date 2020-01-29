const validator = require('express-validator');
var router = require('express').Router()
var users = require('./users')
var movies = require('./movies')

const error = res => {
    return err => res.status(500).send({message:err||'Internal Server Error'})
}

const errorAsync = res => {
    return err => err.trim() // pensar em como fazer as rotas async
}

router.get("/", (req, res) => {
    res.send({'msg':'API do CP Scraper'})
});

router = users(router, validator, error)
router = movies(router, validator, error)

module.exports = router