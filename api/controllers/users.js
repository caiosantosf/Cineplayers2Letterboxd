const db = require("./../../models")
const User = db.users
const Op = db.Sequelize.Op
const scraping = require('./../../services/scraping')

exports.create = async (req, res) => {
  const { cpUserId, email } = req.body

  user = await scraping.getUser(cpUserId)
  user = {...user, cpUserId, email}

  console.log(user)

  /*User.create(user)
    .then(data => {
      res.send(data)
    })
    .catch(err => {
      res.status(500).send({
        message:
          err.message || "Some error occurred while creating the User."
      })
    })  */
}

exports.findAll = (req, res) => {
  
}

exports.findOne = (req, res) => {
  
}

exports.update = (req, res) => {
  
}

exports.delete = (req, res) => {
  
}

exports.deleteAll = (req, res) => {
  
}
/*
exports.createWatchlist = (req, res) => {
    //scraping.
}
*/