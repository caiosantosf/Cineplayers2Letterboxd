const db = require("./../../models")
const User = db.users
const Op = db.Sequelize.Op
const scraping = require('./../../services/scraping')

exports.create = (req, res, error) => {
  const { cpUserId, email } = req.body

  scraping.getUser(cpUserId, user => {
    User.create({...user, cpUserId, email})
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