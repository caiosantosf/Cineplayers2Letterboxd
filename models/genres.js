module.exports = (sequelize, Sequelize) => {
    const Genre = sequelize.define("genres", {
      description: {
        type: Sequelize.STRING
      }
    })
  
    return Genre
  }