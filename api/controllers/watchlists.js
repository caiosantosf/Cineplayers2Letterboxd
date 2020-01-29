const db = require("../../models")
const User = db.users
const Movie = db.movies
const Op = db.Sequelize.Op
const scraping = require('../../services/scraping')

exports.create = (req, res, error) => {
    const id = req.params.id
    const { cpUserId } = await User.findByPk(id)
  
    scraping.getWatchlist(cpUserId, movie => {
      Movie.findByPk(movie.id)
    }, error)
}

exports.findOne = (req, res) => {
  const id = req.params.id

  User.findByPk(id)
    .then(data => res.send(data))
    .catch(err => error(err.message))
}

exports.update = (req, res) => {}

exports.delete = (req, res) => {}

exports.deleteAll = (req, res) => {}
