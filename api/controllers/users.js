const db = require("./../../models")
const User = db.users
const scraping = require('./../../services/scraping')

exports.create = (req, res, error) => {
  const { cpUsername, email } = req.body

  scraping.getUser(cpUsername, user => {
    const cpUserId = user.picture.split('/')[10]

    if (!+cpUserId)
      error()
      
    User.create({...user, cpUserId, cpUsername, email})
      .then(data => res.status(201).send(data))
      .catch(err => error(err.message))
  }, error)
}

exports.findOne = (req, res, error) => {
  const id = req.params.id

  User.findByPk(id)
    .then(data => data ? res.send(data) : res.status(404).send())
    .catch(err => error(err.message))
}

exports.delete = (req, res, error) => {
  const id = req.params.id;

  User.destroy({where: { id: id }})
    .then(_ => res.status(204).send())
    .catch(err => error(err.message))
}