module.exports = (sequelize, Sequelize) => {
  const Rating = sequelize.define("ratings", {
    cpUserId: {type: Sequelize.INTEGER},
    cpMovieId: {type: Sequelize.INTEGER},
    score: {type: Sequelize.INTEGER}
  })
  
  return Rating
  }