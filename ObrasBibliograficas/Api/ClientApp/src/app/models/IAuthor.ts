export default interface IAuthor {
  ID: string;
  name: string;
  formattedName: string;
  createdAt: Date;
  updatedAt: Date;
  formattedCreatedAt?: string;
  formattedUpdatedAt?: string;
}
