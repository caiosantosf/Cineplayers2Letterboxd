const validator = require('express-validator');
var router = require('express').Router()
var users = require('./users')
var ratings = require('./ratings')
var watchlists = require('./watchlists')

const error = res => {
    return err => //res.status(500).send({message:err||'Internal Server Error'})
    console.log(err)
}

router.get("/", (req, res) => {
    res.send({'msg':'API do CP Scraper'})
});

router = users(router, validator, error)
router = ratings(router, validator, error)
//router = watchlists(router, validator, error)

module.exports = router