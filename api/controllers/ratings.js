const db = require("./../../models")
const Rating = db.ratings
const scraping = require('./../../services/scraping')

exports.findAllByCpUserId = (req, res, error) => {
  const cpUserId = req.params.cpUserId

  scraping.getRatings(cpUserId, rating => {
    console.log(rating)
  
    /*Rating
      .findOne({ where: {cpUserId: cpUserId, cpMovieId: rating.cpMovieId} })
      .then(async dbRating => {
          dbRating ? await dbRating.update(rating) : await Rating.create(rating)
      })
      .catch(err => error(err.message))*/
  }, error)

  /*Rating
    .findAll({where: {cpUserId: cpUserId}})
    .then(data => data ? res.send(data) : res.status(404).send())
    .catch(err => error(err.message))*/
}